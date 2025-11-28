"use client";

import { Championship } from "@/interfaces/championship";
import { ChampionshipService } from "@/services/championshipService";
import { Button, Card, Empty, Spin, Typography } from "antd";
import { useRouter } from "next/navigation";
import { useEffect, useState } from "react";

const { Title, Text } = Typography;

export function ChampionshipsContent() {
    const [championships, setChampionships] = useState<Championship[]>([]);
    const [loading, setLoading] = useState<boolean>(false);
    const route = useRouter();

    useEffect(() => {
        async function loadChampionships() {
            try {
                setLoading(true);
                const championshipsData = await ChampionshipService.findAll();
                setChampionships(championshipsData);
            } catch (error) {
                console.error(error);
            } finally {
                setLoading(false);
            }
        }

        loadChampionships();
    }, []);

    return (
        <div className="flex flex-col gap-4">
            <div>
                <Title level={2} className="!m-0">
                    Campeonatos em Andamento
                </Title>
                <Title level={4} type="secondary" className="!m-0 !mt-1">
                    Acompanhe os campeonatos universitários
                </Title>
            </div>
            
            <Spin spinning={loading}>
                <div className="flex flex-col gap-4 mt-4">
                    {championships?.length > 0 ? (
                        championships.map((item) => (
                            <Card key={item.id} className="shadow-sm hover:shadow-md transition-shadow">
                                <div className="flex justify-between items-center flex-wrap gap-4">
                                    <div className="flex flex-col">
                                        <Title level={4} className="!m-0">
                                            {item.name}
                                        </Title>
                                        <Text className="text-gray-500">{item.university}</Text>
                                    </div>
                                    <div>
                                        <Button
                                            type="primary"
                                            onClick={() =>
                                                route.push(`/app/championships/${item.id}`)
                                            }
                                        >
                                            Ver detalhes
                                        </Button>
                                    </div>
                                </div>
                            </Card>
                        ))
                    ) : (
                        <Empty description={"Sem dados"} />
                    )}
                </div>
            </Spin>
        </div>
    );
}
