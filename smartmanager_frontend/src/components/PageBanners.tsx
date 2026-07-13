type PageBannersProps = {
  message?: string | null;
  error?: string | null;
};

/** 성공/오류 피드백용 화면 배너. alert 대신 사용한다. */
export default function PageBanners({ message, error }: PageBannersProps) {
  if (!message && !error) {
    return null;
  }
  return (
    <>
      {message ? <p className="success-banner">{message}</p> : null}
      {error ? <p className="error-banner">{error}</p> : null}
    </>
  );
}
