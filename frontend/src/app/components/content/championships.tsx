"use client";

import { Championship } from "@/app/interfaces/championship";
import { ChampionshipService } from "@/app/services/championshipService";
import { Button, Card, Empty, Flex, Spin, Typography } from "antd";
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
        <Flex flex={1} vertical gap={4}>
            <Title level={2} style={{ margin: 0 }}>
                Campeonatos em Andamento
            </Title>
            <Title level={4} type="secondary" style={{ margin: 0 }}>
                Acompanhe os campeonatos universitários
            </Title>
            <Spin spinning={loading}>
                <Flex vertical gap={4} style={{ marginTop: "1rem" }}>
                    {championships?.length > 0 ? (
                        championships.map((item) => (
                            <Card key={item.id}>
                                <Flex justify="space-between" align="center">
                                    <Flex vertical>
                                        <Title level={4} style={{ margin: 0 }}>
                                            {item.name}
                                        </Title>
                                        <Text>{item.university}</Text>
                                    </Flex>
                                    <Flex>
                                        <Button
                                            type="primary"
                                            onClick={() =>
                                                route.push(`/app/championships/${item.id}`)
                                            }
                                        >
                                            Ver detalhes
                                        </Button>
                                    </Flex>
                                </Flex>
                            </Card>
                        ))
                    ) : (
                        <Empty description={"Sem dados"} />
                    )}
                </Flex>
            </Spin>
        </Flex>
    );
}
