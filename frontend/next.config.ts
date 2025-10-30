import type { NextConfig } from "next";

const nextConfig: NextConfig = {
    rewrites: async () => {
        const backendUrl = process.env.BACKEND_URL;

        return [
            {
                source: "/api/:path*",
                destination: `${backendUrl}/:path*`,
            },
        ];
    },
};

export default nextConfig;
