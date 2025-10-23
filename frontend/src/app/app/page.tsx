"use client";

import { TabsProps } from "antd";
import { Header } from "../components/header/header";
import { useHeader } from "../context/headerContext";
import { useEffect, useMemo } from "react";
import { Championships } from "../components/app/content/championships";
import { LayoutDefault } from "../components/layout/layoutDefault";
import { ContentLayout } from "../components/app/content/contentLayout";

const TABS_CONFIG = [
    { key: "1", label: "Início", searchHidden: true, placeholder: "" },
    { key: "2", label: "Campeonatos", searchHidden: false, placeholder: "Buscar campeonatos..." },
    { key: "3", label: "Equipes", searchHidden: false, placeholder: "Buscar equipes..." },
    { key: "4", label: "Resultados", searchHidden: true, placeholder: "" },
] as const;

export default function App() {
    const itemsTabs = useMemo(
        (): TabsProps["items"] => TABS_CONFIG.map(({ key, label }) => ({ key, label })),
        []
    );

    return (
        <LayoutDefault
            header={<Header itemsTabs={itemsTabs} defaultActiveKey="2" />}
            content={<ContentBodyApp />}
        />
    );
}

function ContentBodyApp() {
    const { keyItemTab, configureHeader } = useHeader();

    useEffect(() => {
        if (keyItemTab) {
            const config = TABS_CONFIG[parseInt(keyItemTab) - 1];

            if (config) {
                configureHeader({
                    placeholder: config.placeholder,
                    hidden: config.searchHidden,
                });
            }
        }
    }, [keyItemTab, configureHeader]);

    const renderContent = () => {
        switch (keyItemTab) {
            case "1":
                return <div>Início Content</div>;
            case "2":
                return <Championships />;
            case "3":
                return <div>Equipes Content</div>;
            case "4":
                return <div>Resultados Content</div>;
            default:
                return <div>Selecione uma aba</div>;
        }
    };
    return <ContentLayout>{renderContent()}</ContentLayout>;
}
