"use client";

import { Flex, Drawer, Button } from "antd";
import { Icon } from "@iconify/react";
import { Notification } from "./notification";
import { AvatarIcon } from "./avatar";
import { TabsHeader } from "./tabs";
import { usePathname, useRouter } from "next/navigation";
import { useEffect, useMemo, useState } from "react";
import { MenuOutlined } from "@ant-design/icons";

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
    const [mobileMenuOpen, setMobileMenuOpen] = useState(false);
    const pathname = usePathname();
    const router = useRouter();

    const tabs = useMemo(() => {
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
            setMobileMenuOpen(false);
        }
    };

    return (
        <>
            <header className="h-16 border-b border-gray-light-2 bg-background shadow-sm sticky top-0 z-40">
                <div className="w-full max-w-[1200px] mx-auto px-4 h-full flex justify-between items-center">
                    {/* Logo */}
                    <Flex gap={8} align="center" className="cursor-pointer" onClick={() => router.push("/app")}>
                        <Icon icon="iconoir:graduation-cap" className="text-2xl text-blue" />
                        <h2 className="font-bold text-lg hidden sm:block">Cscore</h2>
                    </Flex>

                    {/* Desktop Navigation */}
                    <div className="hidden md:block flex-1 mx-8">
                        <TabsHeader items={tabs} activeKey={activeTab} setActiveTab={handleTabChange} />
                    </div>

                    {/* Actions */}
                    <Flex gap={16} align="center">
                        <Notification />
                        <AvatarIcon />
                        
                        {/* Mobile Menu Button */}
                        <Button 
                            type="text" 
                            icon={<MenuOutlined />} 
                            className="md:hidden"
                            onClick={() => setMobileMenuOpen(true)}
                        />
                    </Flex>
                </div>
            </header>

            {/* Mobile Navigation Drawer */}
            <Drawer
                title={
                    <Flex gap={8} align="center">
                        <Icon icon="iconoir:graduation-cap" className="text-2xl text-blue" />
                        <span className="font-bold">Cscore</span>
                    </Flex>
                }
                placement="right"
                onClose={() => setMobileMenuOpen(false)}
                open={mobileMenuOpen}
                width={280}
            >
                <div className="flex flex-col gap-2">
                    {TABS_CONFIG.map((tab) => (
                        <Button
                            key={tab.key}
                            type={activeTab === tab.key ? "primary" : "text"}
                            className="w-full justify-start text-left"
                            disabled={tab.disabled}
                            onClick={() => handleTabChange(tab.key)}
                        >
                            {tab.label}
                        </Button>
                    ))}
                </div>
            </Drawer>
        </>
    );
}
