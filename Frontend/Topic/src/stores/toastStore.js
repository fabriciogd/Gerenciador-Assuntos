import { defineStore } from 'pinia';

export const useToastStore = defineStore('toast', {
    state: () => ({
        toasts: [],
    }),
    actions: {
        addToast(toast) {
            this.toasts.push(toast);
            setTimeout(() => {
                this.removeToast(toast.id);
            }, toast.duration || 3000);
        },
        removeToast(id) {
            this.toasts = this.toasts.filter(toast => toast.id !== id);
        },
    },
});