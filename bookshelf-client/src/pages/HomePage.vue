<template>
	<div class="sidebar">
		<CurrentlyReadingList />
	</div>
</template>

<script>
	import CurrentlyReadingList from "@/components/Home/ReadingProgress/CurrentlyReadingList.vue";
	export default {
		components: {
			CurrentlyReadingList,
		},
	};
</script>

<style lang="scss" scoped>
	@import "./HomePage";
</style>
