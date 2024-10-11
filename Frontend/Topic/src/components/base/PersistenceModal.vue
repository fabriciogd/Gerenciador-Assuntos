<template>
	<v-dialog :modelValue="value" :width="width" class="tile" scrollable persistent>
		<v-card tile>
			<v-card-title>
				{{ title }}
			</v-card-title>
			<v-divider />
			<v-card-text :style="{ height: height }">
				<slot></slot>
			</v-card-text>
			<v-divider></v-divider>
			<v-card-actions class="justify-end py-3 mr-3">
				<v-btn v-if="!hiddenNegative" color="gray" text class="px-5" @click="reject"> {{ negative }}</v-btn>
				<v-btn v-if="!hiddenPositive" color="primary" class="px-5" @click="accept">{{ positive }}</v-btn>
			</v-card-actions>
		</v-card>
	</v-dialog>
</template>

<script>
export default {
	name: 'PersistenceModal',
	props: {
		value: {
			type: Boolean,
			default: false,
		},
		title: {
			type: String,
			default: '',
		},
		hiddenPositive: {
			type: Boolean,
			default: false,
		},
		positive: {
			type: String,
			default: '',
		},
		hiddenNegative: {
			type: Boolean,
			default: false,
		},
		negative: {
			type: String,
			default: '',
		},
		width: {
			type: Number,
			default: 500,
		},
		height: {
			type: [String,  Number],
			default: 200,
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