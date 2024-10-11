<template>
    <PersistenceModal :value="value" :title="title" :positive="buttonText" negative="Cancelar" @accept="onAccept"
        @reject="onReject">
        <v-form ref="form">
            <v-row>
                <v-col>
                    <v-text-field v-model="item.title" label="Assunto" outlined clearable hide-details="auto"
                        :rules="[rules.required]"></v-text-field>
                </v-col>
            </v-row>
            <v-row>
                <v-col>
                    <v-select v-model="item.status" :items="statuses" item-title="label" item-value="id" label="Status"
                        outlined clearable dense hide-details="auto" :rules="[rules.required]" />
                </v-col>
            </v-row>
            <v-row>
                <v-col>
                    <v-combobox v-model="item.keywords" dense outlined label="Palavras chaves" chips closable-chips
                        clearable multiple :rules="[rules.required]" hide-details="auto">
                        <template v-slot:selection="{ attrs, item, select, selected }">
                            <v-chip v-bind="attrs" :model-value="selected" @click="select" @click:close="remove(item)">
                            </v-chip>
                        </template>
                    </v-combobox>
                </v-col>
            </v-row>
        </v-form>
    </PersistenceModal>
</template>
<script>
import PersistenceModal from '@/components/base/PersistenceModal.vue'
import { insert, update, getById } from "@/services/topic.service";

export default {
    name: 'CreateForm',
    components: {
        PersistenceModal
    },
    props: {
        value: {
            type: Boolean,
            default: false,
        },
        id: {
            type: String,
            default: null,
        },
    },
    data() {
        return {
            title: "Adicionar assunto",
            buttonText: "Salvar",
            item: {
                title: '',
                status: 1,
                keywords: null
            },
            statuses: [
                {
                    id: 1,
                    label: "Pendente"
                },
                {
                    id: 2,
                    label: "Em progresso"
                },
                {
                    id: 3,
                    label: "Finalizado"
                }
            ],
            rules: {
                required: (value) => !!value || 'Campo obrigatório',
            },
        }
    },
    watch: {
        async value(open) {
            this.title = "Adicionar assunto";

            if (open === false) {
                this.$refs.form.reset();

                this.item = {
                    title: '',
                    status: 1,
                    keywords: null
                };

                return;
            }

            if (this.id) {
                this.title = "Atualizar assunto";

                const { data } = await getById(this.id);
                this.item = data;
            }
        }
    },
    methods: {
        remove(item) {
            this.item.keywords.splice(this.item.keywords.indexOf(item), 1)
        },
        onReject() {
            this.$emit('close')
        },
        async onAccept() {
            this.$refs.form.validate();

            if (this.$refs.form.isValid) {
                this.id == null
                    ? await insert(this.item)
                    : await update(this.id, this.item)

                this.$emit("close");
                this.$emit("finished");
            }
        }
    }
}
</script>