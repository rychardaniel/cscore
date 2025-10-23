import type { Metadata } from "next";
import { Work_Sans } from "next/font/google";
import "./globals.css";
import { AntdRegistry } from "@ant-design/nextjs-registry";

const workSans = Work_Sans({
    subsets: ["latin"],
    variable: "--font-work-sans",
    weight: ["400", "700"],
});

export const metadata: Metadata = {
    title: "Cscore",
    description: "Application for live academic games",
    icons: "favicon.svg",
};

export default function RootLayout({
    children,
}: Readonly<{
    children: React.ReactNode;
}>) {
    return (
        <html lang="pt-BR">
            <body className={`${workSans.className} antialiased`}>
                <AntdRegistry>{children}</AntdRegistry>
            </body>
        </html>
    );
}
