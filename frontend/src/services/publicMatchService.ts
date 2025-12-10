/* eslint-disable no-undef */
import { Match, MatchEvent, MatchScore, MatchStatus, SportType } from "@/interfaces/match";

export interface MatchFilters {
    championshipId?: number;
    sportType?: SportType;
    status?: MatchStatus;
    date?: string;
    page?: number;
    pageSize?: number;
}

class PublicMatchService {
    async getMatches(filters: MatchFilters = {}): Promise<Match[]> {
        const params = new URLSearchParams();

        if (filters.championshipId)
            params.append("championshipId", filters.championshipId.toString());
        if (filters.sportType) params.append("sportType", filters.sportType.toString());
        if (filters.status) params.append("status", filters.status.toString());
        if (filters.date) params.append("date", filters.date);
        if (filters.page) params.append("page", filters.page.toString());
        if (filters.pageSize) params.append("pageSize", filters.pageSize.toString());

        const response = await fetch(`/api/public/matches?${params.toString()}`);

        if (!response.ok) {
            throw new Error("Erro ao buscar partidas");
        }

        return response.json();
    }

    async getMatchById(id: number): Promise<Match> {
        const response = await fetch(`/api/public/matches/${id}`);

        if (!response.ok) {
            if (response.status === 404) {
                throw new Error("Partida não encontrada");
            }
            throw new Error("Erro ao buscar partida");
        }

        return response.json();
    }

    async getLiveMatches(): Promise<Match[]> {
        const response = await fetch(`/api/public/matches/live`);

        if (!response.ok) {
            throw new Error("Erro ao buscar partidas ao vivo");
        }

        return response.json();
    }

    async getMatchEvents(matchId: number): Promise<MatchEvent[]> {
        const response = await fetch(`/api/public/matches/${matchId}/events`);

        if (!response.ok) {
            throw new Error("Erro ao buscar eventos da partida");
        }

        return response.json();
    }

    async getMatchScore(matchId: number): Promise<MatchScore | null> {
        try {
            const response = await fetch(`/api/public/matches/${matchId}/score`);

            if (response.status === 404) {
                return null; // No score yet
            }

            if (!response.ok) {
                throw new Error("Erro ao buscar placar");
            }

            return response.json();
        } catch (error) {
            console.error("Error fetching match score:", error);
            return null;
        }
    }
}

export const publicMatchService = new PublicMatchService();
