import { Flex, Avatar, Popover, Button } from "antd";
import { Icon } from "@iconify/react";
import { useState } from "react";

interface AvatarProps {}

export function AvatarIcon({}: AvatarProps) {
    const [open, setOpen] = useState(false);

    const handleOpenChange = (newOpen: boolean) => {
        setOpen(newOpen);
    };

    return (
        <Flex>
            <Popover
                content={<AvatarContent />}
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

interface AvatarContentProps {}

function AvatarContent({}: AvatarContentProps) {
    return (
        <div>
            <h4>Avatar</h4>
            <p>Some information about the avatar.</p>
        </div>
    );
}
