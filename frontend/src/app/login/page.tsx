"use client";

import { useAuth } from "@/contexts/AuthContext";
import { LoginRequest } from "@/interfaces/user";
import { Button, Card, Form, Input, Typography } from "antd";
import Link from "next/link";
import { useState, useEffect } from "react";

const { Title, Text } = Typography;

export default function LoginPage() {
    const { login, logout } = useAuth();
    const [loading, setLoading] = useState(false);

    // Clear session when accessing login page
    useEffect(() => {
        logout();
    }, []); // eslint-disable-line react-hooks/exhaustive-deps

    const onFinish = async (values: LoginRequest) => {
        setLoading(true);
        try {
            await login(values);
        } catch (error) {
            // Error handled in context
        } finally {
            setLoading(false);
        }
    };

    return (
        <div className="min-h-dvh flex items-center justify-center bg-gray-50 p-4">
            <Card className="w-full max-w-md shadow-lg">
                <div className="text-center mb-8">
                    <Title level={2} className="!mb-2">Bem-vindo de volta</Title>
                    <Text type="secondary">Insira suas credenciais para acessar sua conta</Text>
                </div>

                <Form
                    name="login"
                    layout="vertical"
                    onFinish={onFinish}
                    autoComplete="off"
                    size="large"
                >
                    <Form.Item
                        label="E-mail"
                        name="email"
                        rules={[
                            { required: true, message: "Por favor insira seu e-mail!" },
                            { type: "email", message: "E-mail inválido!" },
                        ]}
                    >
                        <Input placeholder="seu@email.com" />
                    </Form.Item>

                    <Form.Item
                        label="Senha"
                        name="password"
                        rules={[{ required: true, message: "Por favor insira sua senha!" }]}
                    >
                        <Input.Password placeholder="********" />
                    </Form.Item>

                    <Form.Item>
                        <Button type="primary" htmlType="submit" block loading={loading}>
                            Entrar
                        </Button>
                    </Form.Item>
                </Form>

                <div className="text-center mt-4">
                    <Text>
                        Não tem uma conta?{" "}
                        <Link href="/register" className="text-blue hover:underline">
                            Cadastre-se
                        </Link>
                    </Text>
                </div>
            </Card>
        </div>
    );
}
