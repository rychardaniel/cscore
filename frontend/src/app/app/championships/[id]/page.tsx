export default async function ChampionshipPage({ params }: { params: Promise<{ id: string }> }) {
    const { id } = await params;
    return <h1>ID acessado: {id}</h1>;
}
