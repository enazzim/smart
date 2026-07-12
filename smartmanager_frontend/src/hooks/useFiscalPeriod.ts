import { useEffect, useMemo, useState } from 'react';
import { resolveFiscalPeriod, type FiscalPeriod } from '../utils/fiscalCalendar';

export function useFiscalPeriod(baseDate: string, cutoverSetting: string) {
  const autoPeriod = useMemo(
    () => resolveFiscalPeriod(baseDate, cutoverSetting),
    [baseDate, cutoverSetting],
  );
  const [period, setPeriod] = useState<FiscalPeriod>(() => resolveFiscalPeriod(baseDate, cutoverSetting));
  const [manual, setManual] = useState(false);

  useEffect(() => {
    if (!manual) {
      setPeriod(autoPeriod);
    }
  }, [autoPeriod, manual]);

  useEffect(() => {
    setManual(false);
  }, [cutoverSetting]);

  const onPeriodChange = (next: FiscalPeriod) => {
    setPeriod(next);
    setManual(true);
  };

  const setFromStored = (fiscalYear: number, fiscalMonth: number) => {
    setPeriod({ fiscalYear, fiscalMonth });
    setManual(true);
  };

  const resetToAuto = () => {
    setManual(false);
    setPeriod(autoPeriod);
  };

  return { period, onPeriodChange, setFromStored, resetToAuto, autoPeriod, manual };
}
