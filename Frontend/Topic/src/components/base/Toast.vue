<template>
    <v-snackbar v-model="visible" :timeout="duration" :color="color">
        {{ message }}
        <template v-slot:actions>
            <v-icon color="white" text @click="close">
                mdi-close
            </v-icon>
        </template>
    </v-snackbar>
</template>

<script>
export default {
    props: {
        message: String,
        duration: {
            type: Number,
            default: 3000,
        },
        color: {
            type: String,
            default: 'success',
        },
    },
    data() {
        return {
            visible: true,
        };
    },
    methods: {
        close() {
            this.visible = false;
        },
    },
};
</script>