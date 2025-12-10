import { Card, Tag, Typography } from "antd";
import Image from "next/image";
import { Match, MatchStatus, getSportName, getMatchStatusName } from "@/interfaces/match";

const { Text } = Typography;

interface MatchCardProps {
    match: Match;
}

export function MatchCard({ match }: MatchCardProps) {
    const statusColors: Record<MatchStatus, string> = {
        [MatchStatus.Scheduled]: "blue",
        [MatchStatus.InProgress]: "green",
        [MatchStatus.Finished]: "default",
        [MatchStatus.Canceled]: "red",
        [MatchStatus.Postponed]: "orange",
    };

    const formatDate = (dateString: string) => {
        const date = new Date(dateString);
        return date.toLocaleDateString("pt-BR", {
            day: "2-digit",
            month: "2-digit",
            hour: "2-digit",
            minute: "2-digit",
        });
    };

    return (
        <Card
            className="shadow-sm hover:shadow-md transition-shadow cursor-pointer h-full"
            hoverable
        >
            <div className="flex flex-col gap-3">
                {/* Title and Status */}
                <div className="flex justify-between items-start gap-2">
                    <Text strong className="flex-1 text-base">
                        {match.name}
                    </Text>
                    <Tag
                        color={statusColors[match.status]}
                        className={match.status === MatchStatus.InProgress ? "animate-pulse" : ""}
                    >
                        {getMatchStatusName(match.status)}
                    </Tag>
                </div>

                {/* Championship info */}
                {match.championship && (
                    <Text type="secondary" className="text-sm">
                        {match.championship.name}
                    </Text>
                )}

                {/* Sport */}
                <div className="flex items-center gap-2">
                    <svg
                        className="w-4 h-4 text-gray-500"
                        fill="none"
                        stroke="currentColor"
                        viewBox="0 0 24 24"
                    >
                        <path
                            strokeLinecap="round"
                            strokeLinejoin="round"
                            strokeWidth={2}
                            d="M19 11H5m14 0a2 2 0 012 2v6a2 2 0 01-2 2H5a2 2 0 01-2-2v-6a2 2 0 012-2m14 0V9a2 2 0 00-2-2M5 11V9a2 2 0 012-2m0 0V5a2 2 0 012-2h6a2 2 0 012 2v2M7 7h10"
                        />
                    </svg>
                    <Text className="text-sm">{getSportName(match.sportType)}</Text>
                </div>

                {/* Date */}
                <div className="flex items-center gap-2">
                    <svg
                        className="w-4 h-4 text-gray-500"
                        fill="none"
                        stroke="currentColor"
                        viewBox="0 0 24 24"
                    >
                        <path
                            strokeLinecap="round"
                            strokeLinejoin="round"
                            strokeWidth={2}
                            d="M8 7V3m8 4V3m-9 8h10M5 21h14a2 2 0 002-2V7a2 2 0 00-2-2H5a2 2 0 00-2 2v12a2 2 0 002 2z"
                        />
                    </svg>
                    <Text className="text-sm">{formatDate(match.scheduledDate)}</Text>
                </div>

                {/* Venue */}
                {match.venue && (
                    <div className="flex items-center gap-2">
                        <svg
                            className="w-4 h-4 text-gray-500"
                            fill="none"
                            stroke="currentColor"
                            viewBox="0 0 24 24"
                        >
                            <path
                                strokeLinecap="round"
                                strokeLinejoin="round"
                                strokeWidth={2}
                                d="M17.657 16.657L13.414 20.9a1.998 1.998 0 01-2.827 0l-4.244-4.243a8 8 0 1111.314 0z"
                            />
                        </svg>
                        <Text className="text-sm">{match.venue}</Text>
                    </div>
                )}

                {/* Participants */}
                {match.participants && match.participants.length > 0 && (
                    <div className="pt-3 border-t border-gray-200">
                        <div className="flex justify-between items-center gap-2">
                            {match.participants.slice(0, 2).map((participant) => (
                                <div
                                    key={participant.id}
                                    className="flex items-center gap-2 flex-1"
                                >
                                    {participant.logoUrl && (
                                        <Image
                                            src={participant.logoUrl}
                                            alt={participant.name}
                                            width={24}
                                            height={24}
                                            className="rounded-full object-cover"
                                        />
                                    )}
                                    <Text className="text-sm truncate">{participant.name}</Text>
                                </div>
                            ))}
                        </div>
                    </div>
                )}
            </div>
        </Card>
    );
}
