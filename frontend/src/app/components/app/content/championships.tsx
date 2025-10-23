import { Button, Card, Empty, Flex, Spin, Typography } from "antd";
import { useEffect, useState } from "react";

export function Championships() {
    const [championships, setChampionships] = useState<ChampionshipsInterface[]>([]);
    const [loading, setLoading] = useState<boolean>(false);

    const { Title, Text } = Typography;

    useEffect(() => {
        async function loadChampionships() {
            try {
                setLoading(true);
                const championships = await fetchChampionshipsMock();
                setChampionships(championships);
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
                                        <Button type="primary">Ver detalhes</Button>
                                    </Flex>
                                </Flex>
                            </Card>
                        ))
                    ) : (
                        <Empty description={"Sem dados..."} />
                    )}
                </Flex>
            </Spin>
        </Flex>
    );
}

interface ChampionshipsInterface {
    id: number;
    name: string;
    university: string;
}

function fetchChampionshipsMock(): Promise<Array<ChampionshipsInterface>> {
    return new Promise((resolve) => {
        setTimeout(() => {
            resolve([
                { id: 1, name: "Campeonato de Futebol", university: "Setrem" },
                { id: 2, name: "Campeonato de Basquete", university: "Unijuí" },
                { id: 3, name: "Campeonato de Vôlei", university: "Fasa" },
            ]);
        }, 1000);
    });
}
