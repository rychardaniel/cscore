import { Icon } from "@iconify/react";
import { Alert, Button, Flex, Popover } from "antd";
import { useState } from "react";

export function Notification() {
    const [open, setOpen] = useState(false);

    const handleOpenChange = (newOpen: boolean) => {
        setOpen(newOpen);
    };

    return (
        <Popover
            content={<Content />}
            title="Notificações"
            trigger="click"
            open={open}
            onOpenChange={handleOpenChange}
            placement="bottom"
            arrow
            styles={{ body: { maxWidth: "20rem" } }}
        >
            <Button
                type="text"
                icon={<Icon icon="ic:outline-notifications" className="text-xl" />}
            />
        </Popover>
    );
}

function Content() {
    return (
        <Flex>
            <Alert
                showIcon
                type={"info"}
                title={"Você precisa estar conectado para receber notificações"}
            />
        </Flex>
    );
}
