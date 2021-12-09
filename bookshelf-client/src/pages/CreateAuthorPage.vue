<template>
	<div class="create-author-page__container">
		<CreateAuthorForm />
	</div>
</template>

<script>
	import CreateAuthorForm from "@/components/Author/CreateAuthorForm.vue";

	export default {
		components: {
			CreateAuthorForm,
		},
	};
</script>

<style lang="scss" scoped>
	@import "./CreateAuthorPage";
</style>
