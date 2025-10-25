"use client";

import { Flex } from "antd";
import React from "react";

type LayoutDefaultProps = {
    header: React.ReactNode;
    content: React.ReactNode;
};

export function LayoutDefault({ header, content }: LayoutDefaultProps) {
    return (
        <div className="h-screen">
            {header}
            <div className="h-[calc(100vh-4rem)]">
                <Flex justify="center" align="center" style={{ height: "100%" }}>
                    {content}
                </Flex>
            </div>
        </div>
    );
}
