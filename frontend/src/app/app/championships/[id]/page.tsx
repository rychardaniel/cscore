"use client";

import { useEffect, useState } from "react";
import { useParams, useRouter } from "next/navigation";
import { Button, Card, Empty, Spin, Typography, Select } from "antd";
import { Match, MatchStatus, SportType } from "@/interfaces/match";
import { publicMatchService } from "@/services/publicMatchService";
import { publicChampionshipService } from "@/services/publicChampionshipService";
import { MatchCard } from "@/components/MatchCard";

const { Title, Text } = Typography;

export default function ChampionshipDetailPage() {
    const params = useParams();
    const router = useRouter();
    const championshipId = Number(params.id);

    const [championshipName, setChampionshipName] = useState<string>("");
    const [matches, setMatches] = useState<Match[]>([]);
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState<string | null>(null);

    // Filters
    const [sportFilter, setSportFilter] = useState<SportType | undefined>();
    const [statusFilter, setStatusFilter] = useState<MatchStatus | undefined>();

    useEffect(() => {
        if (championshipId) {
            loadChampionshipData();
        }
    }, [championshipId]);

    useEffect(() => {
        if (championshipId) {
            loadMatches();
        }
    }, [championshipId, sportFilter, statusFilter]);

    const loadChampionshipData = async () => {
        try {
            const championship =
                await publicChampionshipService.getChampionshipById(championshipId);
            setChampionshipName(championship.name);
        } catch (err) {
            console.error("Erro ao carregar campeonato:", err);
        }
    };

    const loadMatches = async () => {
        try {
            setLoading(true);
            setError(null);
            const data = await publicMatchService.getMatches({
                championshipId,
                sportType: sportFilter,
                status: statusFilter,
                pageSize: 50,
            });
            setMatches(data);
        } catch (err) {
            setError("Erro ao carregar partidas. Tente novamente.");
            console.error(err);
        } finally {
            setLoading(false);
        }
    };

    return (
        <div className="flex flex-col gap-4">
            {/* Header */}
            <div>
                <Button
                    type="link"
                    onClick={() => router.push("/app/championships")}
                    className="!px-0 mb-2"
                >
                    ← Voltar para campeonatos
                </Button>
                <Title level={2} className="!m-0">
                    {championshipName || "Campeonato"}
                </Title>
                <Title level={4} type="secondary" className="!m-0 !mt-1">
                    Acompanhe todas as partidas deste campeonato
                </Title>
            </div>

            {/* Filters */}
            <Card className="shadow-sm">
                <div className="flex flex-col gap-4">
                    <Text strong>Filtros</Text>
                    <div className="grid grid-cols-1 md:grid-cols-2 gap-4">
                        <div>
                            <Text className="block mb-2">Esporte</Text>
                            <Select
                                value={sportFilter}
                                onChange={setSportFilter}
                                placeholder="Todos os esportes"
                                allowClear
                                className="w-full"
                            >
                                <Select.Option value={SportType.VolleyballMale}>
                                    Vôlei Masculino
                                </Select.Option>
                                <Select.Option value={SportType.VolleyballFemale}>
                                    Vôlei Feminino
                                </Select.Option>
                                <Select.Option value={SportType.FutsalMale}>
                                    Futsal Masculino
                                </Select.Option>
                                <Select.Option value={SportType.Chess}>Xadrez</Select.Option>
                                <Select.Option value={SportType.FIFA}>FIFA</Select.Option>
                                <Select.Option value={SportType.PingPong}>Ping Pong</Select.Option>
                            </Select>
                        </div>

                        <div>
                            <Text className="block mb-2">Status</Text>
                            <Select
                                value={statusFilter}
                                onChange={setStatusFilter}
                                placeholder="Todos os status"
                                allowClear
                                className="w-full"
                            >
                                <Select.Option value={MatchStatus.Scheduled}>
                                    Agendadas
                                </Select.Option>
                                <Select.Option value={MatchStatus.InProgress}>
                                    Em Andamento
                                </Select.Option>
                                <Select.Option value={MatchStatus.Finished}>
                                    Finalizadas
                                </Select.Option>
                            </Select>
                        </div>
                    </div>
                </div>
            </Card>

            {/* Loading */}
            {loading && (
                <div className="flex justify-center py-12">
                    <Spin size="large" />
                </div>
            )}

            {/* Error */}
            {error && (
                <Card className="bg-red-50 border-red-200">
                    <Text type="danger">{error}</Text>
                </Card>
            )}

            {/* Matches Grid */}
            {!loading && !error && (
                <>
                    {matches.length === 0 ? (
                        <Card className="shadow-sm">
                            <Empty description="Nenhuma partida encontrada neste campeonato" />
                        </Card>
                    ) : (
                        <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-4">
                            {matches.map((match) => (
                                <div
                                    key={match.id}
                                    onClick={() =>
                                        router.push(
                                            `/app/championships/${championshipId}/matches/${match.id}`
                                        )
                                    }
                                >
                                    <MatchCard match={match} />
                                </div>
                            ))}
                        </div>
                    )}
                </>
            )}
        </div>
    );
}
