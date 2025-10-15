import { Icon } from "@iconify/react";
import { Alert, Button, Flex, Popover } from "antd";
import { useState } from "react";

type NotificationProps = {};

export function Notification({}: NotificationProps) {
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
			styles={{body: {maxWidth: "20rem"}}}
        >
            <Button
                type="text"
                icon={<Icon icon="ic:outline-notifications" className="text-2xl" />}
            />
        </Popover>
    );
}

function Content() {
    return (
        <Flex>
			<Alert showIcon type={"info"} title={"Você precisa estar conectado para receber notificações"}/>
        </Flex>
    );
}
