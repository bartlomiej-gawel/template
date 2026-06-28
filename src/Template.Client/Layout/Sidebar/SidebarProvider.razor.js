export function load(key, fallback) {
    try {
        const v = localStorage.getItem(key);
        return v === null ? fallback : v === 'true';
    } catch {
        return fallback;
    }
}

export function save(key, value) {
    try {
        localStorage.setItem(key, value ? 'true' : 'false');
    } catch {
        // ignore storage failures (private mode, quota, etc.)
    }
}
