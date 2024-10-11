<template>
    <div>
        <Toast v-for="toast in toasts" :key="toast.id" :message="toast.message" :duration="toast.duration"
            :color="toast.color" />
    </div>
</template>

<script>
import { defineComponent } from 'vue';
import { useToastStore } from '@/stores/toastStore';
import Toast from './Toast.vue';

export default defineComponent({
    components: {
        Toast,
    },
    data() {
        return {
            toasts: [],
        };
    },
    created() {
        const toastStore = useToastStore();
        this.toasts = toastStore.toasts;

        // Subscribe to changes in the toast store
        toastStore.$subscribe(() => {
            this.toasts = toastStore.toasts;
        });
    },
});
</script>