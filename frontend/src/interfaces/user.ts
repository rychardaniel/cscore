export interface User {
    id: number;
    name: string;
    email: string;
    createdAt: string;
}

export interface LoginRequest {
    email: string;
    password: string;
}

export interface RegisterRequest {
    name: string;
    email: string;
    password: string;
}

export interface LoginResponse {
    message: string;
}

export interface UserResponseDto extends User {
    id: number;
}
