import { Championship } from "@/interfaces/championship";

class PublicChampionshipService {
    async getChampionships(page = 1, pageSize = 20): Promise<Championship[]> {
        const response = await fetch(`/api/public/championships?page=${page}&pageSize=${pageSize}`);

        if (!response.ok) {
            throw new Error("Erro ao buscar campeonatos");
        }

        return response.json();
    }

    async getChampionshipById(id: number): Promise<Championship> {
        const response = await fetch(`/api/public/championships/${id}`);

        if (!response.ok) {
            if (response.status === 404) {
                throw new Error("Campeonato não encontrado");
            }
            throw new Error("Erro ao buscar campeonato");
        }

        return response.json();
    }
}

export const publicChampionshipService = new PublicChampionshipService();
