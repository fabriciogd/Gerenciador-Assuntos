<template>
	<v-container>
		<v-data-table hide-default-footer :headers="headers" :items="topics">
			<template v-slot:top>
				<v-toolbar color="blue-lighten-5">
					<v-btn class="ma-4" :ripple="false" variant="plain" color="primary" dark @click="clickAdd()">
						Adicionar
					</v-btn>
				</v-toolbar>
			</template>
			<template v-slot:item.keywords="{ item }">
				<v-chip-group selected-class="text-primary" mandatory>
					<v-chip v-for="keyword in item.keywords" :key="keyword" :text="keyword" label></v-chip>
				</v-chip-group>
			</template>
			<template v-slot:item.statusDescription="{ item }">
				<v-chip :color="getStatusColor(item.status)" :text="item.statusDescription" class="text-uppercase"
					size="small" label></v-chip>
			</template>
			<template v-slot:item.actions="{ item }">
				<v-icon @click="clickEdit(item.id)">
					mdi-pencil
				</v-icon>
				<v-icon  @click="clickDelete(item.id)">
					mdi-delete
				</v-icon>
				<v-icon :disabled="item.status != 1" @click="clickSearch(item.id)">
					mdi-magnify
				</v-icon>
				<v-icon @click="clickOpenNewspaper(item.id)">
					mdi-newspaper-variant-multiple				
				</v-icon>
			</template>
		</v-data-table>
		<CreateForm :value="openCreateModal" :id="selectedId" @finished="fetch()" @close="openCreateModal = false" />
		<ConfirmationModal :value="openConfirmationModal" message="Você tem certeza que deseja deletar?"
			@reject="openConfirmationModal = false" @accept="onAcceptDelete" />
	</v-container>
</template>

<script>
import { fetch, remove, search } from "@/services/topic.service";
import CreateForm from "@/views/topic/CreateForm.vue";
import ConfirmationModal from "@/components/base/ConfirmationModal.vue";
import { useToastStore } from '@/stores/toastStore';

export default {
	components: {
		CreateForm,
		ConfirmationModal
	},
	name: "TopicView",
	data() {
		return {
			selectedId: null,
			openCreateModal: false,
			openConfirmationModal: false,
			headers: [{
				title: "Assunto",
				key: 'title'
			}, {
				title: "Palavras chave",
				key: 'keywords'
			}, {
				title: "Status",
				key: 'statusDescription'
			}, {
				title: "Ações",
				key: 'actions'
			}],
			topics: []
		}
	},
	methods: {
		getStatusColor(status) {
			return {
				1: 'grey-darken-2',
				2: 'yellow-darken-2',
				3: 'green-darken-2'
			}[status]
		},
		async fetch() {
			const { data } = await fetch();
			this.topics = data;
		},
		clickAdd() {
			this.selectedId = null;
			this.openCreateModal = true;
		},
		clickEdit(id) {
			this.selectedId = id;
			this.openCreateModal = true;
		},
		clickDelete(id) {
			this.selectedId = id;
			this.openConfirmationModal = true;
		},
		clickOpenNewspaper(id) {
			this.$router.push(`/assuntos/${id}/links`)
		},
		async clickSearch(id) {
			await search(id);

			await this.fetch();
		},
		async onAcceptDelete() {
			await remove(this.selectedId);

			this.selectedId = null;
			this.openConfirmationModal = false;
			
			await this.fetch();
		}
	},
	async mounted() {
		await this.fetch();
	},
	errorCaptured(err) {
        const message = 'Um erro ocorreu: ' + err.response.data.errors.map(a => a.message).join(",")

        const toastStore = useToastStore();

        const toast = {
            id: Date.now(), // Ensure this is unique
            message: message,
            duration: 3000,
            color: 'error',
        };

        toastStore.addToast(toast);
    },
}
</script>
