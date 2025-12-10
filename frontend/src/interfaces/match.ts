export enum SportType {
    Soletrando = 1,
    SuperMario = 2,
    Truco = 3,
    VolleyballFemale = 4,
    VolleyballMale = 5,
    VolleyballMixed = 6,
    Chess = 7,
    Bocha48 = 8,
    Canastra = 9,
    FIFA = 10,
    FutsalMale = 11,
    PingPong = 12,
    RelayFemale = 13,
    RelayMale = 14,
}

export enum MatchStatus {
    Scheduled = 1,
    InProgress = 2,
    Finished = 3,
    Canceled = 4,
    Postponed = 5,
}

export enum ParticipantType {
    Individual = 1,
    Team = 2,
}

export enum ParticipantResult {
    Winner = 1,
    Loser = 2,
    Draw = 3,
}

export interface MatchParticipant {
    id: number;
    matchId: number;
    type: ParticipantType;
    name: string;
    side: string; // 'home', 'away', 'player1', 'player2', etc
    logoUrl?: string;
    result?: ParticipantResult;
}

export interface Match {
    id: number;
    name: string;
    championshipId: number;
    championship?: {
        id: number;
        name: string;
        university: string;
    };
    sportType: SportType;
    status: MatchStatus;
    scheduledDate: string;
    startedAt?: string;
    finishedAt?: string;
    venue?: string;
    mongoScoreId?: string;
    notes?: string;
    participants?: MatchParticipant[];
}

export interface MatchScore {
    id: string;
    matchId: number;
    sportType: number;
    scoreData: Record<string, unknown>; // Dynamic structure based on sport type
    updatedAt: string;
    updatedByUserId: number;
}

export interface MatchEvent {
    id: string;
    matchId: number;
    eventType: string;
    occurredAt: string;
    gameMinute?: number;
    participantId?: number;
    details?: Record<string, unknown>;
    registeredByUserId: number;
}

// Sport-specific score structures
export interface VolleyballScore {
    homeScore: number;
    awayScore: number;
    sets: Array<{
        home: number;
        away: number;
    }>;
}

export interface FutsalScore {
    homeScore: number;
    awayScore: number;
}

export interface ChessScore {
    winner?: string; // 'home', 'away', or null for draw
    moves?: string[];
}

// Helper function to get sport name
export function getSportName(sportType: SportType): string {
    const sportNames: Record<SportType, string> = {
        [SportType.Soletrando]: "Soletrando",
        [SportType.SuperMario]: "Super Mário",
        [SportType.Truco]: "Truco",
        [SportType.VolleyballFemale]: "Vôlei Feminino",
        [SportType.VolleyballMale]: "Vôlei Masculino",
        [SportType.VolleyballMixed]: "Vôlei Misto",
        [SportType.Chess]: "Xadrez",
        [SportType.Bocha48]: "48 (Bocha)",
        [SportType.Canastra]: "Canastra",
        [SportType.FIFA]: "FIFA",
        [SportType.FutsalMale]: "Futsal Masculino",
        [SportType.PingPong]: "Ping Pong",
        [SportType.RelayFemale]: "Revezamento Feminino",
        [SportType.RelayMale]: "Revezamento Masculino",
    };
    return sportNames[sportType] || "Desconhecido";
}

// Helper function to get status name
export function getMatchStatusName(status: MatchStatus): string {
    const statusNames: Record<MatchStatus, string> = {
        [MatchStatus.Scheduled]: "Agendada",
        [MatchStatus.InProgress]: "Em Andamento",
        [MatchStatus.Finished]: "Finalizada",
        [MatchStatus.Canceled]: "Cancelada",
        [MatchStatus.Postponed]: "Adiada",
    };
    return statusNames[status] || "Desconhecido";
}
