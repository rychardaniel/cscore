import { LoginRequest, LoginResponse, RegisterRequest, RegisterResponse, User } from "@/interfaces/user";

export const AuthService = {
    async login(data: LoginRequest): Promise<LoginResponse> {
        const response = await fetch("/api/users/login", {
            method: "POST",
            headers: {
                "Content-Type": "application/json",
            },
            body: JSON.stringify(data),
        });

        if (!response.ok) {
            const error = await response.json();
            throw new Error(error.message || "Erro ao fazer login");
        }

        return response.json();
    },

    async register(data: RegisterRequest): Promise<RegisterResponse> {
        const response = await fetch("/api/users/register", {
            method: "POST",
            headers: {
                "Content-Type": "application/json",
            },
            body: JSON.stringify(data),
        });

        if (!response.ok) {
            const error = await response.json();
            throw new Error(error.message || "Erro ao criar usuário");
        }

        return response.json();
    },

    async getMe(): Promise<User> {
        const response = await fetch("/api/users/me");

        if (!response.ok) {
            throw new Error("Não autenticado");
        }

        return response.json();
    },

    async logout(): Promise<void> {
        // Assuming there is a logout endpoint to clear the cookie
        // If not, this might fail or 404, but we should try.
        // If the backend doesn't have it, we just ignore the error.
        try {
            await fetch("/api/users/logout", { method: "POST" });
        } catch (error) {
            // Ignore error on logout
        }
    },
};
