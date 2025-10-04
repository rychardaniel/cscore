"use client";

import { ConfigProvider } from "antd";
import { useServerInsertedHTML } from "next/navigation";
import { StyleProvider, createCache, extractStyle } from "@ant-design/cssinjs";
import { useState } from "react";

type AntdProps = {
    children: React.ReactNode;
};

export function AntdProvider({ children }: AntdProps) {
    const [cache] = useState(() => createCache());

    useServerInsertedHTML(() => (
        <style
            id="antd"
            dangerouslySetInnerHTML={{ __html: extractStyle(cache, true) }}
        />
    ));

    return (
        <StyleProvider cache={cache}>
            <ConfigProvider
                theme={{
                    token: {
                        colorBgContainer: "#ededed",
                        colorText: "#0a0a0a",
                        borderRadius: 12,
                    },
                }}
            >
                {children}
            </ConfigProvider>
        </StyleProvider>
    );
}
