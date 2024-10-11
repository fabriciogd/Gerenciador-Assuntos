<template>
    <v-container>
        <v-data-iterator :items="links" :page="page" items-per-page="5">
            <template v-slot:default="{ items }">
                <template v-for="item in items" :key="item.id">
                    <v-card elevation="12" class="mx-auto py-3">
                        <v-card-title>
                            {{ item.raw.title || "Sem título" }}
                        </v-card-title>
                        <v-card-text>
                            {{ item.raw.description || "Sem descrição" }}
                        </v-card-text>
                        <v-card-subtitle> 
                            <a target="_blank" :href="item.raw.url">Acessar</a>
                        </v-card-subtitle>
                    </v-card>
                    <br>
                </template>
            </template>
        </v-data-iterator>
        <v-pagination v-model="page" :length="pageLength" rounded="circle"></v-pagination>
    </v-container>
</template>

<script>
import { fetchLinks } from "@/services/topic.service";

export default {
    name: "NewspaperView",
    data() {
        return {
            page: 1,
            links: []
        }
    },
    computed: {
        pageLength() {
            return this.links.length / 5
        }
    },
    async mounted() {
        // react to route changes...
        await this.fetch(this.$route.params.id)
    },
    methods: {
        async fetch(id) {
            debugger
            const { data } = await fetchLinks(id);
            this.links = data;
        },
    }
}
</script>