"use client";

import { User, LoginRequest, RegisterRequest } from "@/interfaces/user";
import { AuthService } from "@/services/authService";
import { useRouter } from "next/navigation";
import { createContext, useContext, useState, ReactNode } from "react";
import { message } from "antd";

interface AuthContextType {
    user: User | null;
    login: (data: LoginRequest) => Promise<void>;
    register: (data: RegisterRequest) => Promise<void>;
    logout: () => void;
    isAuthenticated: boolean;
}

const AuthContext = createContext<AuthContextType | undefined>(undefined);

export function AuthProvider({ children }: { children: ReactNode }) {
    const [user, setUser] = useState<User | null>(null);
    const router = useRouter();

    // Restore session on mount
    useState(() => {
        const restoreSession = async () => {
            try {
                const user = await AuthService.getMe();
                setUser(user);
            } catch (error) {
                // Not authenticated, just ignore
            }
        };
        restoreSession();
    });

    const login = async (data: LoginRequest) => {
        try {
            await AuthService.login(data);
            // Fetch user details after successful login
            const user = await AuthService.getMe();
            setUser(user);
            message.success("Login realizado com sucesso!");
            router.push("/app");
        } catch (error) {
            message.error(error instanceof Error ? error.message : "Erro ao fazer login");
            throw error;
        }
    };

    const register = async (data: RegisterRequest) => {
        try {
            await AuthService.register(data);
            message.success("Conta criada com sucesso! Faça login.");
            router.push("/login");
        } catch (error) {
            message.error(error instanceof Error ? error.message : "Erro ao criar conta");
            throw error;
        }
    };

    const logout = async () => {
        await AuthService.logout();
        setUser(null);
        router.push("/login");
    };

    return (
        <AuthContext.Provider value={{ user, login, register, logout, isAuthenticated: !!user }}>
            {children}
        </AuthContext.Provider>
    );
}

export function useAuth() {
    const context = useContext(AuthContext);
    if (context === undefined) {
        throw new Error("useAuth must be used within an AuthProvider");
    }
    return context;
}
