<template>
    <v-card height="100" :color="color">
        <v-card-title class="text-h5">
            {{ title }}
        </v-card-title>

        <v-card-text>{{ value }} assuntos</v-card-text>
    </v-card>
</template>
<script>
export default {
    name: 'InformationCard',
	props: {
        color: {
			type: String,
			default: '',
        },
		title: {
			type: String,
			default: '',
		},
		value: {
			type: Number,
			default: 0,
		},
    }
}
</script>