import { createRouter, createWebHistory } from 'vue-router'

const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes: [
    {
      path: '/',
      name: 'home',
      component: () => import('../views/DashboardView.vue')
    },
    {
      path: '/assuntos',
      name: 'assuntos',
      component: () => import('../views/TopicView.vue')
    },
    { path: '/assuntos/:id/links', name: 'links', component: () => import('../views/NewspaperView.vue') },
  ]
})

export default router
