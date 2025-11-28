"use client";

import { useAuth } from "@/contexts/AuthContext";
import { RegisterRequest } from "@/interfaces/user";
import { Button, Card, Form, Input, Typography } from "antd";
import Link from "next/link";
import { useState } from "react";

const { Title, Text } = Typography;

export default function RegisterPage() {
    const { register } = useAuth();
    const [loading, setLoading] = useState(false);

    const onFinish = async (values: RegisterRequest) => {
        setLoading(true);
        try {
            await register(values);
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
                    <Title level={2} className="!mb-2">Crie sua conta</Title>
                    <Text type="secondary">Preencha os dados abaixo para começar</Text>
                </div>

                <Form
                    name="register"
                    layout="vertical"
                    onFinish={onFinish}
                    autoComplete="off"
                    size="large"
                >
                    <Form.Item
                        label="Nome Completo"
                        name="name"
                        rules={[{ required: true, message: "Por favor insira seu nome!" }]}
                    >
                        <Input placeholder="Seu nome" />
                    </Form.Item>

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
                        rules={[{ required: true, message: "Por favor insira uma senha!" }]}
                    >
                        <Input.Password placeholder="********" />
                    </Form.Item>

                    <Form.Item>
                        <Button type="primary" htmlType="submit" block loading={loading}>
                            Criar Conta
                        </Button>
                    </Form.Item>
                </Form>

                <div className="text-center mt-4">
                    <Text>
                        Já tem uma conta?{" "}
                        <Link href="/login" className="text-blue hover:underline">
                            Faça login
                        </Link>
                    </Text>
                </div>
            </Card>
        </div>
    );
}
