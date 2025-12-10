import { Flex, Avatar, Popover, Button, Typography, Divider } from "antd";
import { Icon } from "@iconify/react";
import { useState } from "react";
import { useAuth } from "@/contexts/AuthContext";
import { useRouter } from "next/navigation";

const { Text, Title } = Typography;

export function AvatarIcon() {
    const [open, setOpen] = useState(false);
    const { user } = useAuth();
    const router = useRouter();

    const handleOpenChange = (newOpen: boolean) => {
        setOpen(newOpen);
    };

    if (!user) {
        return (
            <Button type="primary" onClick={() => router.push("/login")}>
                Entrar
            </Button>
        );
    }

    return (
        <Flex>
            <Popover
                content={<AvatarContent onClose={() => setOpen(false)} />}
                trigger="click"
                open={open}
                onOpenChange={handleOpenChange}
                placement="bottomRight"
                arrow
            >
                <Button
                    type="text"
                    shape="circle"
                    icon={<Avatar size={32} icon={<Icon icon="material-symbols:person" />} />}
                />
            </Popover>
        </Flex>
    );
}

function AvatarContent({ onClose }: { onClose: () => void }) {
    const { user, logout } = useAuth();

    const handleLogout = () => {
        logout();
        onClose();
    };

    return (
        <div className="min-w-[200px]">
            <div className="px-2 py-1">
                <Title level={5} className="!m-0">
                    {user?.name || "Usuário"}
                </Title>
                <Text type="secondary" className="text-xs">
                    {user?.email}
                </Text>
            </div>
            <Divider className="my-2" />
            <Button
                type="text"
                danger
                block
                onClick={handleLogout}
                className="text-left justify-start"
            >
                <Flex gap={8} align="center">
                    <Icon icon="material-symbols:logout" />
                    Sair
                </Flex>
            </Button>
        </div>
    );
}
