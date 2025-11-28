"use client";

import React from "react";

type LayoutDefaultProps = {
    header: React.ReactNode;
    content: React.ReactNode;
};

export function LayoutDefault({ header, content }: LayoutDefaultProps) {
    return (
        <div className="flex flex-col min-h-dvh bg-background text-foreground">
            <div className="flex-none z-50 bg-background">
                {header}
            </div>
            <main className="flex-1 w-full max-w-[1200px] mx-auto p-4 md:p-6">
                {content}
            </main>
        </div>
    );
}
