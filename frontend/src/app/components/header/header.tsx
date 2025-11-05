"use client";

import { Flex, TabsProps } from "antd";
import { Icon } from "@iconify/react";
import { Notification } from "./notification";
import { AvatarIcon } from "./avatar";
import { TabsHeader } from "./tabs";
import { usePathname, useRouter } from "next/navigation";
import { useEffect, useMemo, useState } from "react";
import { Grid } from "antd";
const { useBreakpoint } = Grid;

const TABS_CONFIG = [
    {
        key: "1",
        label: "Início",
        path: "/app",
        searchHidden: true,
        placeholder: "",
        disabled: false,
    },
    {
        key: "2",
        label: "Campeonatos",
        path: "/app/championships",
        searchHidden: false,
        placeholder: "Buscar campeonatos...",
        disabled: false,
    },
    {
        key: "3",
        label: "Equipes",
        path: "/app/TODO",
        searchHidden: false,
        placeholder: "Buscar equipes...",
        disabled: true,
    },
    {
        key: "4",
        label: "Resultados",
        path: "/app/TODO",
        searchHidden: true,
        placeholder: "",
        disabled: true,
    },
] as const;

export function Header() {
    const [activeTab, setActiveTab] = useState<string>("");
    const pathname = usePathname();
    const router = useRouter();
    const screens = useBreakpoint();

    const tabs: TabsProps["items"] = useMemo(() => {
        return TABS_CONFIG.map(({ key, label, disabled }) => ({ key, label, disabled }));
    }, []);

    useEffect(() => {
        let currentTab = TABS_CONFIG.find((tab) => pathname === tab.path);

        if (!currentTab) {
            currentTab = TABS_CONFIG.find(
                (tab) => tab.path !== "/app" && pathname.startsWith(tab.path)
            );
        }

        if (currentTab && currentTab.key !== activeTab) {
            setActiveTab(currentTab.key);
        }
    }, [pathname, activeTab]);

    const handleTabChange = (key: string) => {
        const tab = TABS_CONFIG.find((t) => t.key === key);
        if (tab) {
            setActiveTab(key);
            router.push(tab.path);
        }
    };

    if (screens.md === undefined) return <></>

    if (screens.md) {
        return (
            <Flex
                justify="center"
                align="center"
                style={{
                    height: 64,
                    borderBottom: "1px solid var(--gray-light-2)",
                    boxShadow: "0 1px 2px 0 rgba(0, 0, 0, 0.05)",
                }}
            >
                <div className="w-full max-w-[1200px] p-4 h-full flex justify-between items-center gap-4">
                    <Flex gap={5}>
                        <Icon icon="iconoir:graduation-cap" className="text-2xl text-(--blue)" />
                        <h2 className="font-bold">Cscore</h2>
                    </Flex>
                    <TabsHeader items={tabs} activeKey={activeTab} setActiveTab={handleTabChange} />
                    <Flex gap={"1rem"}>
                        <Notification />
                        <AvatarIcon />
                    </Flex>
                </div>
            </Flex>
        );
    }

    // Mobile Header
    return (
        <Flex
            justify="center"
            align="center"
            style={{
                height: 128,
                borderBottom: "1px solid var(--gray-light-2)",
                boxShadow: "0 1px 2px 0 rgba(0, 0, 0, 0.05)",
            }}
        >
            <Flex vertical justify="center" align="center" flex={1}>
                <div className="w-full max-w-[1200px] p-4 h-full flex justify-between items-center gap-4">
                    <Flex justify="space-between" style={{ width: "100%" }} align="center">
                        <Flex gap={5}>
                            <Icon
                                icon="iconoir:graduation-cap"
                                className="text-2xl text-(--blue)"
                            />
                            <h2 className="font-bold">Cscore</h2>
                        </Flex>
                    </Flex>
                    <Notification />
                    <AvatarIcon />
                </div>
                <TabsHeader items={tabs} activeKey={activeTab} setActiveTab={handleTabChange} />
            </Flex>
        </Flex>
    );
}
