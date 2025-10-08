import HeaderProvider from "../components/app/header";

export default function RootLayout({
    children,
}: Readonly<{
    children: React.ReactNode;
}>) {
    return (
        <div className="h-screen">
            <HeaderProvider>{children}</HeaderProvider>
        </div>
    );
}
