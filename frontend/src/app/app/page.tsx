"use client";

import { RightOutlined } from "@ant-design/icons";
import { Button, Card, Typography } from "antd";
import { useRouter } from "next/navigation";

export default function App() {
    const router = useRouter();

    return (
        <div className="flex flex-col gap-4">
            <div className="grid grid-cols-1 md:grid-cols-2 gap-4">
                <Card className="shadow-sm">
                    <div className="flex justify-center">
                        <Typography.Title level={4} className="!m-0">
                            Bem-vindo ao Cscore!
                        </Typography.Title>
                    </div>
                </Card>
                <Card className="shadow-sm">
                    <div className="flex justify-center">
                        <Button
                            type="link"
                            icon={<RightOutlined />}
                            onClick={() => router.push("/app/championships")}
                        >
                            Ir para campeonatos
                        </Button>
                    </div>
                </Card>
            </div>
        </div>
    );
}
