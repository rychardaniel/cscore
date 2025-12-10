"use client";

import { useEffect, useState } from "react";
import { useParams, useRouter } from "next/navigation";
import { Button, Card, Empty, Spin, Typography, Tag, Divider } from "antd";
import { Match, MatchEvent, getSportName, getMatchStatusName } from "@/interfaces/match";
import { publicMatchService } from "@/services/publicMatchService";
import { ScoreDisplay } from "@/components/ScoreDisplay";

const { Title, Text } = Typography;

export default function MatchDetailPage() {
    const params = useParams();
    const router = useRouter();
    const championshipId = Number(params.id);
    const matchId = Number(params.matchId);

    const [match, setMatch] = useState<Match | null>(null);
    const [events, setEvents] = useState<MatchEvent[]>([]);
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState<string | null>(null);

    useEffect(() => {
        if (matchId) {
            loadMatchData();
        }
    }, [matchId]);

    const loadMatchData = async () => {
        try {
            setLoading(true);
            setError(null);

            const [matchData, eventsData] = await Promise.all([
                publicMatchService.getMatchById(matchId),
                publicMatchService.getMatchEvents(matchId),
            ]);

            setMatch(matchData);
            setEvents(eventsData);
        } catch (err: any) {
            setError(err.message || "Erro ao carregar dados da partida");
            console.error(err);
        } finally {
            setLoading(false);
        }
    };

    const formatDate = (dateString: string) => {
        const date = new Date(dateString);
        return date.toLocaleDateString("pt-BR", {
            day: "2-digit",
            month: "2-digit",
            year: "numeric",
            hour: "2-digit",
            minute: "2-digit",
        });
    };

    if (loading) {
        return (
            <div className="flex justify-center items-center py-12">
                <Spin size="large" />
            </div>
        );
    }

    if (error || !match) {
        return (
            <div className="flex flex-col gap-4">
                <Card className="bg-red-50 border-red-200">
                    <Text type="danger">{error || "Partida não encontrada"}</Text>
                </Card>
                <Button
                    type="primary"
                    onClick={() => router.push(`/app/championships/${championshipId}`)}
                >
                    Voltar para o campeonato
                </Button>
            </div>
        );
    }

    return (
        <div className="flex flex-col gap-4">
            {/* Back Button */}
            <Button
                type="link"
                onClick={() => router.push(`/app/championships/${championshipId}`)}
                className="!px-0"
            >
                ← Voltar para o campeonato
            </Button>

            {/* Match Header */}
            <Card className="shadow-sm">
                <div className="flex justify-between items-start flex-wrap gap-4">
                    <div className="flex-1">
                        <Title level={2} className="!m-0 !mb-2">
                            {match.name}
                        </Title>
                        {match.championship && (
                            <Text type="secondary">
                                {match.championship.name} - {match.championship.university}
                            </Text>
                        )}
                    </div>
                    <Tag
                        color={match.status === 2 ? "green" : "blue"}
                        className="text-sm px-3 py-1"
                    >
                        {getMatchStatusName(match.status)}
                    </Tag>
                </div>

                <Divider />

                <div className="grid grid-cols-1 md:grid-cols-3 gap-4">
                    <div>
                        <Text type="secondary" className="block mb-1">
                            Esporte
                        </Text>
                        <Text strong>{getSportName(match.sportType)}</Text>
                    </div>

                    <div>
                        <Text type="secondary" className="block mb-1">
                            Data/Hora
                        </Text>
                        <Text strong>{formatDate(match.scheduledDate)}</Text>
                    </div>

                    {match.venue && (
                        <div>
                            <Text type="secondary" className="block mb-1">
                                Local
                            </Text>
                            <Text strong>{match.venue}</Text>
                        </div>
                    )}
                </div>
            </Card>

            {/* Participants */}
            {match.participants && match.participants.length > 0 && (
                <Card title="Participantes" className="shadow-sm">
                    <div className="grid grid-cols-1 md:grid-cols-2 gap-4">
                        {match.participants.map((participant) => (
                            <Card key={participant.id} className="bg-gray-50">
                                <div className="flex items-center gap-3">
                                    {participant.logoUrl && (
                                        <img
                                            src={participant.logoUrl}
                                            alt={participant.name}
                                            className="w-12 h-12 rounded-full object-cover"
                                        />
                                    )}
                                    <div className="flex-1">
                                        <Text strong className="block">
                                            {participant.name}
                                        </Text>
                                        <Text type="secondary" className="text-sm capitalize">
                                            {participant.side}
                                        </Text>
                                    </div>
                                    {participant.result && (
                                        <Tag
                                            color={
                                                participant.result === 1
                                                    ? "green"
                                                    : participant.result === 2
                                                      ? "red"
                                                      : "default"
                                            }
                                        >
                                            {participant.result === 1
                                                ? "Vencedor"
                                                : participant.result === 2
                                                  ? "Perdedor"
                                                  : "Empate"}
                                        </Tag>
                                    )}
                                </div>
                            </Card>
                        ))}
                    </div>
                </Card>
            )}

            {/* Score */}
            <Card title="Placar" className="shadow-sm">
                <ScoreDisplay sportType={match.sportType} scoreData={null} />
            </Card>

            {/* Events */}
            {events.length > 0 && (
                <Card title="Eventos da Partida" className="shadow-sm">
                    <div className="space-y-3">
                        {events.map((event) => (
                            <Card key={event.id} className="bg-gray-50">
                                <div className="flex items-start gap-3">
                                    {event.gameMinute !== undefined && (
                                        <Tag color="blue">{event.gameMinute}'</Tag>
                                    )}
                                    <div className="flex-1">
                                        <Text strong className="block capitalize">
                                            {event.eventType}
                                        </Text>
                                        <Text type="secondary" className="text-sm">
                                            {new Date(event.occurredAt).toLocaleTimeString("pt-BR")}
                                        </Text>
                                    </div>
                                </div>
                            </Card>
                        ))}
                    </div>
                </Card>
            )}
        </div>
    );
}
