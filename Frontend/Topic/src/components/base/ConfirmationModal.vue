<template>
    <v-dialog :modelValue="value" persistent width="500">
        <v-card>
            <v-card-title class="font-weight-bold">
                <v-avatar class="mr-3">
                    <v-icon color="error">mdi-trash-can-outline</v-icon>
                </v-avatar>
                {{ title }}
            </v-card-title>
            <v-card-text class="text-center my-5">{{ message }}</v-card-text>
            <v-divider></v-divider>
            <v-card-actions class="justify-end py-3 mr-3">
                <v-btn cancel text class="px-5" @click="reject">{{ negative }}</v-btn>
                <v-btn error class="px-5" @click="accept">{{ positive }}</v-btn>
            </v-card-actions>
        </v-card>
    </v-dialog>
</template>

<script>
export default {
    props: {
        value: {
            type: Boolean,
            default: false,
        },
        title: {
            type: String,
            default: 'Excluir',
        },
        message: {
            type: String,
            default: 'Você tem certeza?',
        },
        negative: {
            type: String,
            default: 'Cancelar',
        },
        positive: {
            type: String,
            default: 'Sim, excluir',
        },
    },
    methods: {
        accept() {
            this.$emit('accept')
        },
        reject() {
            this.$emit('reject')
        },
    },
}
</script>