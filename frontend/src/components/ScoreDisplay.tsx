import { Card, Typography, Empty } from "antd";
import { SportType, VolleyballScore, FutsalScore, ChessScore } from "@/interfaces/match";

const { Text, Title } = Typography;

interface ScoreDisplayProps {
    sportType: SportType;
    scoreData: unknown;
}

export function ScoreDisplay({ sportType, scoreData }: ScoreDisplayProps) {
    if (!scoreData) {
        return (
            <div className="text-center py-8">
                <Empty description="Placar ainda não disponível" />
            </div>
        );
    }

    // Volleyball (Female, Male, Mixed)
    if (
        sportType === SportType.VolleyballFemale ||
        sportType === SportType.VolleyballMale ||
        sportType === SportType.VolleyballMixed
    ) {
        const score = scoreData as VolleyballScore;
        return (
            <div className="flex flex-col gap-6">
                <div className="flex justify-center items-center gap-8">
                    <div className="text-center">
                        <Title level={1} className="m-0">
                            {score.homeScore}
                        </Title>
                        <Text type="secondary">Casa</Text>
                    </div>
                    <Title level={2} type="secondary" className="m-0">
                        -
                    </Title>
                    <div className="text-center">
                        <Title level={1} className="m-0">
                            {score.awayScore}
                        </Title>
                        <Text type="secondary">Visitante</Text>
                    </div>
                </div>

                {score.sets && score.sets.length > 0 && (
                    <div className="border-t pt-4">
                        <Text strong className="block mb-3">
                            Sets
                        </Text>
                        <div className="space-y-2">
                            {score.sets.map((set, index) => (
                                <Card key={index} size="small" className="bg-gray-50">
                                    <div className="flex justify-between items-center">
                                        <Text>Set {index + 1}</Text>
                                        <Text strong>
                                            {set.home} - {set.away}
                                        </Text>
                                    </div>
                                </Card>
                            ))}
                        </div>
                    </div>
                )}
            </div>
        );
    }

    // Futsal
    if (sportType === SportType.FutsalMale) {
        const score = scoreData as FutsalScore;
        return (
            <div className="flex justify-center items-center gap-8">
                <div className="text-center">
                    <Title level={1} className="m-0">
                        {score.homeScore}
                    </Title>
                    <Text type="secondary">Casa</Text>
                </div>
                <Title level={2} type="secondary" className="m-0">
                    -
                </Title>
                <div className="text-center">
                    <Title level={1} className="m-0">
                        {score.awayScore}
                    </Title>
                    <Text type="secondary">Visitante</Text>
                </div>
            </div>
        );
    }

    // Chess
    if (sportType === SportType.Chess) {
        const score = scoreData as ChessScore;
        return (
            <div className="text-center">
                {score.winner ? (
                    <div>
                        <Title level={3} className="m-0 mb-2">
                            Vencedor: {score.winner === "home" ? "Casa" : "Visitante"}
                        </Title>
                        {score.moves && <Text type="secondary">{score.moves.length} jogadas</Text>}
                    </div>
                ) : (
                    <Title level={3} className="m-0">
                        Empate
                    </Title>
                )}
            </div>
        );
    }

    // Generic fallback for other sports
    return (
        <div className="text-center">
            <pre className="text-sm text-left bg-gray-50 p-4 rounded overflow-x-auto">
                {JSON.stringify(scoreData, null, 2)}
            </pre>
        </div>
    );
}
