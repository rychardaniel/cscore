export const TABS_CONFIG = [
    { key: "1", label: "Início", path: "/app", searchHidden: true, placeholder: "" },
    {
        key: "2",
        label: "Campeonatos",
        path: "/app/championships",
        searchHidden: false,
        placeholder: "Buscar campeonatos...",
    },
    {
        key: "3",
        label: "Equipes",
        path: "/app/3",
        searchHidden: false,
        placeholder: "Buscar equipes...",
    },
    { key: "4", label: "Resultados", path: "/app/4", searchHidden: true, placeholder: "" },
] as const;

export type TabConfig = (typeof TABS_CONFIG)[number];
