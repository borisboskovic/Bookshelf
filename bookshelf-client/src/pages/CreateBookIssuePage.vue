<template>
	<div class="create-book-issue-page__container">
		<div class="page-background"></div>
		<CreateBookIssueForm />
	</div>
</template>

<script>
	import CreateBookIssueForm from "@/components/Books/CreateBookIssueForm.vue";

	export default {
		components: {
			CreateBookIssueForm,
		},
	};
</script>

<style lang="scss" scoped>
	@import "./CreateBookIssuePage.scss";
</style>
