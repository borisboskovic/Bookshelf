<template>
	<div class="input-container">
		<input
			type="text"
			:placeholder="placeholder"
			@input="searchTermChangeHandler"
			:value="searchTerm"
		/>
		<i class="fa fa-search" aria-hidden="true"></i>
	</div>
</template>

<script>
	export default {
		emits: ["search-term-change"],
		props: {
			searchTerm: String,
			placeholder: String,
		},
		setup: (_, context) => {
			const searchTermChangeHandler = (event) => {
				context.emit("search-term-change", event.target.value);
			};

			return {
				searchTermChangeHandler,
			};
		},
	};
</script>

<style lang="scss" scoped>
	@import "./SearchField";
</style>
