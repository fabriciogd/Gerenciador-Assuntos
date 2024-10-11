<script setup>
import { RouterView } from 'vue-router'
import NavBar from './components/base/NavBar.vue'
import ToastContainer from './components/base/ToastContainer.vue';

</script>

<template>
	<ToastContainer/>
	<NavBar/>
	<RouterView />
</template>