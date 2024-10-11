<template>
    <v-toolbar :title="title" app color="primary">
        <v-toolbar-items>
            <v-btn
                variant="plain"
                class="text-none mb-4"
                size="x-large"
                v-for="item in menuItems"
                :key="item.title"
                :to="item.path"
            >
                <v-icon left dark>{{ item.icon }}</v-icon>
                {{ item.title }}
            </v-btn>
        </v-toolbar-items>   
    </v-toolbar>
</template>
<script>
export default {
    name: "NavBar",
    data(){
        return {
            title: 'Gerenciador de assuntos',
            sidebar: false,
            menuItems: [
                { title: 'Dashboard', path: '/', icon: 'mdi-home' },
                { title: 'Assuntos', path: '/assuntos', icon: 'mdi-list-box' },
            ]
        }
    },
};
</script>