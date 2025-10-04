import type { Metadata } from "next";
import localFont from "next/font/local";
import "./globals.css";
import {AntdProvider} from "./providers/AntdProvider";

const geistSans = localFont({
    src: "./fonts/GeistVF.woff",
    variable: "--font-geist-sans",
    weight: "100 900",
    display: "swap",
    preload: true,
});
const geistMono = localFont({
    src: "./fonts/GeistMonoVF.woff",
    variable: "--font-geist-mono",
    weight: "100 900",
    display: "swap",
    preload: true,
});

export const metadata: Metadata = {
    title: "Cscore",
    description: "Live sports scores",
    icons: {
        icon: "/favicon.svg",
    },
};

export default function RootLayout({
    children,
}: Readonly<{
    children: React.ReactNode;
}>) {
    return (
        <html lang="en">
            <body
                className={`${geistSans.variable} ${geistMono.variable} antialiased`}
            >
                <AntdProvider>
                    {children}
                </AntdProvider>
            </body>
        </html>
    );
}
