<template>
	<div class="create-book-page__container">
		<div class="page-background"></div>
		<CreateBookForm />
	</div>
</template>

<script>
	import CreateBookForm from "@/components/Books/CreateBookForm.vue";

	export default {
		components: {
			CreateBookForm,
		},
	};
</script>

<style lang="scss" scoped>
	@import "./CreateBookPage.scss";
</style>
