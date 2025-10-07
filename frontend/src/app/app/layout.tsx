import Header from "../components/app/header";

export default function RootLayout({
    children,
}: Readonly<{
    children: React.ReactNode;
}>) {
    return (
        <div className="h-screen">
            <Header />
            {children}
        </div>
    );
}
