export function parseFilterParam(param: string | string[] | undefined): number[] | undefined {
    if (!param) return undefined;

    if (Array.isArray(param)) {
        const parsed = param.map(Number).filter(Boolean);
        return parsed.length > 0 ? parsed : undefined;
    }

    const parsed = Number(param);
    return isNaN(parsed) ? undefined : [parsed];
}