<template>
	<v-container>
		<v-row>
			<v-col cols="12" md="4">
				<InformationCard title="Pendentes" :value="getCount(1)" color="grey-darken-2"/>
			</v-col>
			<v-col cols="12" md="4">
				<InformationCard title="Em progresso" :value="getCount(2)" color="yellow-darken-2"/>
			</v-col>
			<v-col cols="12" md="4">
				<InformationCard title="Finalizado" :value="getCount(3)" color="green-darken-2"/>
			</v-col>
		</v-row>
	</v-container>
</template>
<script>
import { fetch } from "@/services/dashboard.service";
import InformationCard from '@/components/base/InformationCard.vue'
export default {
    name: 'DashboardView',
	components: {
		InformationCard
	},
	data() {
		return {
			groups: []
		}
	},
	methods: {
		async fetch() {
			const { data } = await fetch();
			this.groups = data;
		},
		getCount(status) {
			return this.groups.find(item => item.status === status)?.count ?? 0;
		}
	},
	async mounted() {
		await this.fetch();
	}
}
</script>