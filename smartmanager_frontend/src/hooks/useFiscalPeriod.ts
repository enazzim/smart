import { useEffect, useMemo, useState } from 'react';
import { resolveFiscalPeriod, type FiscalPeriod } from '../utils/fiscalCalendar';

export function useFiscalPeriod(baseDate: string) {
  const autoPeriod = useMemo(() => resolveFiscalPeriod(baseDate), [baseDate]);
  const [period, setPeriod] = useState<FiscalPeriod>(() => resolveFiscalPeriod(baseDate));
  const [manual, setManual] = useState(false);

  useEffect(() => {
    if (!manual) {
      setPeriod(autoPeriod);
    }
  }, [autoPeriod, manual]);

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
