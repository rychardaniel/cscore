import { Championship } from "../interfaces/championship";

export const ChampionshipService = {
    async findAll(): Promise<Championship[]> {
        const response = await fetch("/api/championships", { cache: "no-store" });
        if (!response.ok) throw new Error("Erro ao buscar campeonatos");
        return response.json();
    },

    async findOne(id: number): Promise<Championship> {
        const response = await fetch(`/api/championships/${id}`, { cache: "no-store" });
        if (!response.ok) throw new Error("Erro ao buscar campeonato");
        return response.json();
    },

    async create(championship: Championship): Promise<Championship> {
        const response = await fetch("/api/championships", {
            method: "POST",
            headers: { "Content-Type": "application/json" },
            body: JSON.stringify(championship),
        });

        if (!response.ok) throw new Error("Erro ao criar campeonato");
        return response.json();
    },

    async update(id: number, championship: Championship): Promise<Championship> {
        const response = await fetch(`/api/championships/${id}`, {
            method: "PUT",
            headers: { "Content-Type": "application/json" },
            body: JSON.stringify(championship),
        });

        if (!response.ok) throw new Error("Erro ao atualizar campeonato");
        return response.json();
    },

    async delete(id: number): Promise<boolean> {
        const response = await fetch(`/api/championships/${id}`, { method: "DELETE" });
        return response.ok;
    },
};
