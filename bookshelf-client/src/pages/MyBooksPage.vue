<template>
	My Books
</template>

<script>
	import { onMounted } from "vue";
	export default {
		setup: () => {
			onMounted(() => {
				document.title = "BookSelf - My Books";
			});
		},
	};
</script>

<style lang="scss" scoped></style>
