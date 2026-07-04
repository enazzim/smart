interface PlaceholderPageProps {
  title: string;
}

export default function PlaceholderPage({ title }: PlaceholderPageProps) {
  return (
    <>
      <header>
        <h1>{title}</h1>
        <p>해당 기능은 준비 중입니다.</p>
      </header>
      <section className="panel placeholder-panel">
        <p>Phase 1 이후 영역에서 구현 예정입니다.</p>
      </section>
    </>
  );
}
